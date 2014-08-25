
using HisFeldLibrary.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace HisFeldTry1.Service
{
    public class StorageService
    {
        DataContractJsonSerializer serializerJson = new DataContractJsonSerializer(typeof(TaskBook));
        XmlSerializer serializerXml = new XmlSerializer(typeof(TaskBook));
        Timer timer;

        public TaskBook TaskBookOutOfStorage;
        public void BeginnSaveTimer()
        {
            timer = new Timer(
                (niet) =>
                {
                    SaveToStorage();

                }, null,20000, 10000);
        }

        public void SaveToStorage()
        {
            try
            {
                IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream saveStream = new IsolatedStorageFileStream("TaskBook.xml", FileMode.OpenOrCreate, fileStorage);

                //StreamWriter Writer = new StreamWriter(new IsolatedStorageFileStream("TaskBook.json", FileMode.OpenOrCreate, fileStorage));
                serializerXml.Serialize(saveStream, TaskBookOutOfStorage);
                //serializer.WriteObject(Writer.BaseStream, TaskBookOutOfStorage);
                //Writer.Close();
                saveStream.Close();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }

        public void LoadFromStorage()
        {
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            //StreamReader Reader = null;
            try
            {
                IsolatedStorageFileStream saveStream = new IsolatedStorageFileStream("TaskBook.xml", FileMode.Open, fileStorage);
                //Reader = new StreamReader(new IsolatedStorageFileStream("TaskBook.json", FileMode.OpenOrCreate, fileStorage));

                //TaskBookOutOfStorage = serializerJson.ReadObject(Reader.BaseStream) as TaskBook;
                TaskBookOutOfStorage = serializerXml.Deserialize(saveStream) as TaskBook;
                
                //Reader.Close();
                saveStream.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
            finally
            {
                if (TaskBookOutOfStorage == null)
                {
                    TaskBookOutOfStorage = new TaskBook();
                }
            }
        }


    }
}
