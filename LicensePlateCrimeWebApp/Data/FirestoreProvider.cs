using Google.Apis.Util;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;

namespace LicensePlateCrimeWebApp.Data
{
    public class FirestoreProvider
    {
        private readonly FirestoreDb _fireStoreDb = null!;
        private readonly StorageClient _storageClient = null!;
        private readonly UrlSigner _urlSigner = null!;
        private readonly FirebaseSettings _firebaseSettings;
        private string GetCollectionName<T>()
        {
            return $"{typeof(T).Name}s";
        }
        public FirestoreProvider(FirestoreDb fireStoreDb, StorageClient storageClient, FirebaseSettings firebaseSettings, UrlSigner urlSigner)
        {
            _fireStoreDb = fireStoreDb;
            _storageClient = storageClient;
            _firebaseSettings = firebaseSettings;
            _urlSigner = urlSigner;
        }

        public async Task<bool> AddOrUpdate<T>(T entity)
        {
            CollectionReference collection = _fireStoreDb.Collection(GetCollectionName<T>());
            DocumentReference document = await collection.AddAsync(entity);
            return document != null;
        }

        public async Task<T> Get<T>(string id)
        {
            var document = _fireStoreDb.Collection(GetCollectionName<T>()).Document(id);
            var snapshot = await document.GetSnapshotAsync();
            return snapshot.ConvertTo<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>()
        {
            var collection = _fireStoreDb.Collection(GetCollectionName<T>());
            var snapshot = await collection.GetSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value)
        {
            return await GetList<T>(_fireStoreDb.Collection(GetCollectionName<T>()).WhereEqualTo(fieldPath, value));
        }

        // just add here any method you need here WhereGreaterThan, WhereIn etc ...

        private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query)
        {
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        // Storage Stuff
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            using var stream = imageFile.OpenReadStream();
            var obj = await _storageClient.UploadObjectAsync(_firebaseSettings.StorageBucketName, imageFile.FileName, imageFile.ContentType, stream);
            //string signedUrl = await _urlSigner.SignAsync(_firebaseSettings.StorageBucketName, obj.Name, TimeSpan.FromHours(1));
            string publicUrl = GetPublicUrl(obj.Bucket, obj.Name);
            return publicUrl;
        }

        private string GetPublicUrl(string bucketName, string fileName)
        {
            return $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{fileName}?alt=media";
        }
    }
}
