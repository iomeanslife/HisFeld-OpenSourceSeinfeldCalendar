
using HisFeldLibrary.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;

namespace HisFeldRT.Service
{
    public class StorageService
    {
        DataContractJsonSerializer serializerJson = new DataContractJsonSerializer(typeof(TaskBook));
        XmlSerializer serializerXml = new XmlSerializer(typeof(TaskBook));
        Timer timer;

        private string name = "TaskBook.xml";
        private StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
        private CreationCollisionOption option = Windows.Storage.CreationCollisionOption.ReplaceExisting;

        public TaskBook TaskBookOutOfStorage;
        public void BeginnSaveTimer()
        {
            timer = new Timer(
                (niet) =>
                {
                    SaveToStorage();

                }, null,20000, 10000);
        }

        public async void SaveToStorage()
        {
            try
            {
                StringWriter saveWriter = new StringWriter();

                serializerXml.Serialize(saveWriter, TaskBookOutOfStorage);
 
                var file = await folder.CreateFileAsync(name, option);

                if (file == null)
                {
                    throw new Exception();
                }

                await Windows.Storage.FileIO.WriteTextAsync(file, saveWriter.ToString());
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }

        public async void LoadFromStorage()
        {
            try
            {
                var file = await folder.GetFileAsync(name);

                if (file == null)
                {
                    throw new Exception();
                }

                string fileContent = await FileIO.ReadTextAsync(file);

                StringReader loadReader = new StringReader(fileContent);

                TaskBookOutOfStorage = serializerXml.Deserialize(loadReader) as TaskBook;
                
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
