using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibInMemoryFileSystem
{
    public class CloakroomFolderEntry
    {
        public CloakroomFolderEntry()
        {
            Files = new List<CloakroomFileEntry>();
        }

        public List<CloakroomFileEntry> Files { get; set; }
    }

    public class CloakroomFileEntry
    {
        public CloakroomFileEntry()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public byte[] FileContents { get; set; }
    }

    public class Cloakroom
    {
        private CloakroomFolderEntry root;

        public Cloakroom()
        {
            root = new CloakroomFolderEntry();
        }

        public Guid Save(byte[] data)
        {
            CloakroomFileEntry fe = new CloakroomFileEntry() { FileContents = data };
            root.Files.Add(fe);
            return fe.Id;
        }

        public byte[] Read(Guid id)
        {
            for (int i = 0; i < root.Files.Count; i++)
            {
                if (root.Files[i].Id == id)
                    return root.Files[i].FileContents;
            }

            return null;
        }

        public void Delete(Guid id)
        {
            for (int i = 0; i < root.Files.Count; i++)
            {
                if (root.Files[i].Id == id)
                    root.Files.RemoveAt(i);
            }
        }

        public void Clear()
        {
            root.Files.Clear();
        }

        public string Backup()
        {
            return JsonConvert.SerializeObject(root);
        }

        public void Restore(string backup)
        {
            root = JsonConvert.DeserializeObject<CloakroomFolderEntry>(backup);
        }
    }
}
