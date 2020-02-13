using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography;
using System.IO;

namespace FindFile
{
    static class LoadDataManager
    {
        static public void UploadData<T>(T data, string path)
        {
            string json = JsonSerializer.Serialize(data);

            //using(SHA256 secret = SHA256.Create())
            //{}

            using(StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                //Encoding enc = Encoding.UTF8;
                //byte[] hashValue = secret.ComputeHash(enc.GetBytes(json));
                //for(int i =0; i< hashValue.Length; i++)
                //{
                //    sw.Write(hashValue[i]);
                //}

                sw.Write(json);
            }
            
        }

        static public T LoadData<T>(string path)
        {
            string json = string.Empty;

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    //using(SHA256 secret = SHA256.Create())
                    //{
                    //}
                    json = sr.ReadToEndAsync().Result;
                }

                return JsonSerializer.Deserialize<T>(json);
            }
            else
                return default(T);
            
        }





    }
}
