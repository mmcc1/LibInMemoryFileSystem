using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibInMemoryFileSystem
{
    public class CloakroomFolderEntry2<T>
    {
        public CloakroomFolderEntry2()
        {
            Files = new List<CloakroomFileEntry2<T>>();
        }

        public List<CloakroomFileEntry2<T>> Files { get; set; }
    }

    public class CloakroomFileEntry2<T>
    {
        public CloakroomFileEntry2()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public T FileContents { get; set; }
    }

    public class CloakroomGeneric<T>
    {
        private CloakroomFolderEntry2<T> root;

        public CloakroomGeneric()
        {
            root = new CloakroomFolderEntry2<T>();
        }

        public Guid Save(T data)
        {
            CloakroomFileEntry2<T> fe = new CloakroomFileEntry2<T>() { FileContents = data };
            root.Files.Add(fe);
            return fe.Id;
        }

        public dynamic Read(Guid id)
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
            root = JsonConvert.DeserializeObject<CloakroomFolderEntry2<T>>(backup);
        }
    }
}
