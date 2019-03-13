using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LevelEditor
{
    public class LanguageManager
    {
        /// <summary>
        /// Ref. to LevelEditor Controller
        /// </summary>
        public LevelEditor_Controller Controller;
        /// <summary>
        /// Ref. to Language Dictionary
        /// </summary>
        public LanguageDictionary Dictionary;


        /// <summary>
        /// The Language Manager
        /// </summary>
        /// <param name="_controller"></param>
        public LanguageManager(LevelEditor_Controller _controller)
        {
            Controller = _controller;

           // Dictionary = new LanguageDictionary(new List<LanguageEntry>());
           // Dictionary.Dict.Add(new LanguageEntry("#Test", new LanguageEntryMeanings("Der Test", "The Test", "Le Test", "El Test")));
           // Dictionary.Dict.Add(new LanguageEntry("#MenuItem_File", new LanguageEntryMeanings("Datei", "File", "Le File", "El File")));
           // Dictionary.Dict.Add(new LanguageEntry("#MenuItem_NewLevel", new LanguageEntryMeanings("Neues Level", "New Level", "Le Help", "El Helpo")));
        }

        /// <summary>
        /// This function gets the KEY ID from the Language.xml
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetDictValue(string _id)
        {
            LanguageEntry FoundEntry = new LanguageEntry();

            foreach (LanguageEntry entry in Dictionary.Dict)
            {
                if (entry.ID == _id)
                {
                    FoundEntry = entry;
                    break;
                }
            }

            switch (Controller.Model.Config.Language)
            {
                default:
                case LevelEditorLanguage.German:
                    return FoundEntry.Meanings.Entry_German;
                case LevelEditorLanguage.English:
                    return FoundEntry.Meanings.Entry_English;
                case LevelEditorLanguage.Turkish:
                    return FoundEntry.Meanings.Entry_Turkish;
            }
        }


        /// <summary>
        /// Saving the Dictionary
        /// </summary>
        public void SaveDictionary()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Config"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Config");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(LanguageDictionary));
            using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + @"\Config\Languages.xml"))
            {
                serializer.Serialize(writer, Dictionary);
            }
        }

        /// <summary>
        /// Loading the Dictionary
        /// </summary>
        public void LoadDictionary()
        {
            if (!File.Exists(Environment.CurrentDirectory + @"\Config\Config.xml"))
            {
                SaveDictionary();
                return;
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(LanguageDictionary));
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Config\Languages.xml");
            Dictionary = (LanguageDictionary)deserializer.Deserialize(reader);
            reader.Close();
        }
    }

    /// <summary>
    /// Struct of the Language Dictionary
    /// </summary>
    [Serializable]
    public struct LanguageDictionary
    {
        public List<LanguageEntry> Dict;

        public LanguageDictionary(List<LanguageEntry> _Dict)
        {
            Dict = _Dict;
        }
    }

    /// <summary>
    /// Struct of the Language Entry(s)
    /// </summary>
    [Serializable]
    public struct LanguageEntry
    {
        [XmlAttribute]
        public string ID;
        public LanguageEntryMeanings Meanings;

        public LanguageEntry(string _ID, LanguageEntryMeanings _meanings)
        {
            ID = _ID;
            Meanings = _meanings;
        }
    }

    /// <summary>
    /// Struct of the Language Meanings
    /// </summary>
    [Serializable]
    public struct LanguageEntryMeanings
    {
        [XmlAttribute]
        public string Entry_German;
        [XmlAttribute]
        public string Entry_English;
        [XmlAttribute]
        public string Entry_Turkish;

        public LanguageEntryMeanings(string _german, string _english, string _turkish)
        {
            Entry_German = _german;
            Entry_English = _english;
            Entry_Turkish = _turkish;
        }
    }
}
