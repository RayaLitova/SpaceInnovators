using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSys : MonoBehaviour
{
    public static void Save(CurrentProgress c){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.data";
        CurrentProgressSerialized data = new CurrentProgressSerialized(c);
        FileStream fs = File.Create(path);
        fs.Position = 0;
        formatter.Serialize(fs,data);
        fs.Close();
    }

    public static CurrentProgressSerialized Load(){
        string path = Application.persistentDataPath + "/progress.data";

        if(!File.Exists(path)){
            Debug.LogError("Error loading file!");
            return null;
        }else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            fs.Position = 0;
            try{
                CurrentProgressSerialized data = (CurrentProgressSerialized) formatter.Deserialize(fs);
                fs.Close();
                return data;
            }catch{
                fs.Close();
                return null;
            }
            
        }

    }

}