using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibInMemoryFileSystem
{
    public class CloakroomFolderEntry<T>
    {
        public CloakroomFolderEntry()
        {
            Files = new List<CloakroomFileEntry<T>>();
        }

        public List<CloakroomFileEntry<T>> Files { get; set; }
    }

    public class CloakroomFileEntry<T>
    {
        public CloakroomFileEntry()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public T FileContents { get; set; }
    }

    public class Cloakroom<T>
    {
        private CloakroomFolderEntry<T> root;

        public Cloakroom()
        {
            root = new CloakroomFolderEntry<T>();
        }

        public Guid Save(T data)
        {
            CloakroomFileEntry<T> fe = new CloakroomFileEntry<T>() { FileContents = data };
            root.Files.Add(fe);
            return fe.Id;
        }

        public T Read(Guid id)
        {
            for (int i = 0; i < root.Files.Count; i++)
            {
                if (root.Files[i].Id == id)
                    return root.Files[i].FileContents;
            }

            return (T)Activator.CreateInstance(typeof(T), null);
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
            root = JsonConvert.DeserializeObject<CloakroomFolderEntry<T>>(backup);
        }
    }
}
