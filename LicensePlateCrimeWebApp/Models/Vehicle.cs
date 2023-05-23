﻿using Google.Cloud.Firestore;
using LicensePlateCrimeWebApp.Interfaces;

namespace LicensePlateCrimeWebApp.Models
{
  [FirestoreData]
  public class Vehicle : IFirestoreEntity
  {
    public string Id { get; set; } = "";

    [FirestoreProperty]
    public string OwnerId { get; set; }

    //public Firebase.Auth.User? Owner { get; set; }

    public bool IsWanted { get; set; }

    [FirestoreProperty]
    public string Model { get; set; }

    [FirestoreProperty]
    public string LicensePlate { get; set; }

    [FirestoreProperty]
    public string ImageUrl { get; set; }

    [FirestoreProperty]
    public DateTime? CreationDate { get; set; }

    [FirestoreProperty]
    public DateTime? LastUpdateDate { get; set; }
    public DateTime? GetLocalCreationDate => CreationDate?.ToLocalTime();
    public DateTime? GetLocalLastUpdatedDate => LastUpdateDate?.ToLocalTime();

    public Vehicle(string ownerId, string model, string licensePlate, string imageUrl, bool isWanted = false)
    {
      OwnerId = ownerId;
      IsWanted = isWanted;
      Model = model;
      LicensePlate = licensePlate;
      ImageUrl = imageUrl;
      CreationDate = DateTime.UtcNow;
      LastUpdateDate = DateTime.UtcNow;
    }
    public Vehicle()
    {

    }
  }
}
