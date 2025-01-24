using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileSystemManager 
{
    private string dataDirPath="";
    public FileSystemManager(string dataDirPath){
        this.dataDirPath=dataDirPath;
    }

    public SaveData QuickLoad(){
        string fullpath=Path.Combine(dataDirPath,"quicksave.txt");
        return Load(fullpath);
    }
    public SaveData Load(string fullpath){
        SaveData loadedData=new SaveData();
        if(File.Exists(fullpath)){

                string dataToLoad="";
                using (FileStream stream = new FileStream(fullpath,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream)){
                        dataToLoad=reader.ReadToEnd();  
                    }
                }

                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
        }
        return loadedData;
    }



    public void Save(SaveData data,string fileName){
        string fullpath=Path.Combine(dataDirPath,fileName+".txt");
        Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
        string dataToStore=JsonUtility.ToJson(data,true);

        using(FileStream stream=new FileStream(fullpath,FileMode.Create)){

            using (StreamWriter writer=new StreamWriter(stream)){
                writer.Write(dataToStore);
            }
        }
    }

}
