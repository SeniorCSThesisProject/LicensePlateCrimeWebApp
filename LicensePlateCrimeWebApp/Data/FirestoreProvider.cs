﻿using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using LicensePlateCrimeWebApp.Interfaces;

namespace LicensePlateCrimeWebApp.Data
{
  public class FirestoreProvider
  {
    private readonly FirestoreDb _fireStoreDb = null!;
    private readonly StorageClient _storageClient = null!;
    private readonly FirebaseSettings _firebaseSettings;
    private string GetCollectionName<T>()
    {
      return $"{typeof(T).Name}s";
    }
    public FirestoreProvider(FirestoreDb fireStoreDb, StorageClient storageClient, FirebaseSettings firebaseSettings)
    {
      _fireStoreDb = fireStoreDb;
      _storageClient = storageClient;
      _firebaseSettings = firebaseSettings;
    }

    public async Task<string> AddOrUpdate<T>(T entity) where T : IFirestoreEntity
    {
      CollectionReference collection = _fireStoreDb.Collection(GetCollectionName<T>());
      DocumentReference document = await collection.AddAsync(entity);
      return document.Id;
    }

    public async Task<T> Get<T>(string id) where T : IFirestoreEntity
    {
      var document = _fireStoreDb.Collection(GetCollectionName<T>()).Document(id);
      var snapshot = await document.GetSnapshotAsync();
      return snapshot.ConvertTo<T>();
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : IFirestoreEntity
    {
      var collection = _fireStoreDb.Collection(GetCollectionName<T>());
      var snapshot = await collection.GetSnapshotAsync();
      //return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
      return snapshot.Documents.Select(x => { var entity = x.ConvertTo<T>(); entity.Id = x.Id; return entity; }).ToList();
    }

    public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value) where T : IFirestoreEntity
    {
      return await GetList<T>(_fireStoreDb.Collection(GetCollectionName<T>()).WhereEqualTo(fieldPath, value));
    }

    // just add here any method you need here WhereGreaterThan, WhereIn etc ...

    private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query) where T : IFirestoreEntity
    {
      var snapshot = await query.GetSnapshotAsync();
      return snapshot.Documents.Select(x => { var entity = x.ConvertTo<T>(); entity.Id = x.Id; return entity; }).ToList();
      //return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
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
