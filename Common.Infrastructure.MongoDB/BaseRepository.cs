// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using EnsureThat;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Infrastructure.MongoDB
{
    /// <summary>
    /// This class contains the base class for the database repository classes.
    /// </summary>
    public class BaseRepository
    {
        protected readonly IMongoDatabase MongoDatabase;

        protected BaseRepository(MongoDbSettings settings)
        {
            Ensure.That(settings).IsNotNull();
            Ensure.String.IsNotNullOrEmpty(settings.ServerConnection);
            Ensure.String.IsNotNullOrEmpty(settings.Database);

            var client = new MongoClient(settings.ServerConnection);

            MongoDatabase = client.GetDatabase(settings.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });
        }
    }
}