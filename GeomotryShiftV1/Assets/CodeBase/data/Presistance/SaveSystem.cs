//Author, Written by Harley Sims updated by Atilla puskas
//Description: Saves game data to disk


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

public static class SaveSystem 
{
    public static void SaveGameData(int slot)
    {
        string filePath = GeometryShift.instance.GetDataPath() + "/slot" + slot.ToString() + ".save"; 
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string json = setUpSaveData();
        json = Encrypt(json);
        File.WriteAllText(filePath, json);
    }

    private static string setUpSaveData()
    {
        GroupedData saveData = LevelLoader.instance.GetDataCore().groupedData;
        string jsonData = JsonUtility.ToJson(saveData);
        return jsonData;
    }
    //Creates a home for the save data, This must be called at least once in the lifetime of the program prior to LoadGameData being called!
    //Except when creating a brand new save file.
    public static void InitilizeDataStructure()
    {
        DataCore newDatacore = new DataCore();
        LevelLoader.instance.InitWorldState(newDatacore);
    }

    public static GroupedData LoadGameData(int slot)
    {
        string filePath = GeometryShift.instance.GetDataPath() + "/slot" + slot.ToString() + ".save";
        string json;

        if (File.Exists(filePath))
        {
            try
            {
                json = File.ReadAllText(filePath);             
                json = Decrypt(json);
                GroupedData saveData = JsonUtility.FromJson<GroupedData>(json);
                saveData.worldState.ComfirmArraySize();
                saveData.playerData.inventory_.ConfirmData();
                return saveData;
            }
            catch
            {
                return null;
            }
      
        }
        else
        {
            Debug.Log("File is missing " + filePath);
            return null;
        }
    }

    //Hash for the encrpytion *shhhhhh* it's a secret
    private static string hash = "LockItupBoys1234";
    //Encrypt
    private static string Encrypt(string input)
    {

        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using (MD5CryptoServiceProvider mds = new MD5CryptoServiceProvider())
        {
            byte[] key = mds.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() {Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
    }

    // Decrypt
    private static string Decrypt(string input)
    {

        byte[] data = Convert.FromBase64String(input);
        using (MD5CryptoServiceProvider mds = new MD5CryptoServiceProvider())
        {
            byte[] key = mds.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }
}



