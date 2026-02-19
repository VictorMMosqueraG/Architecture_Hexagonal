namespace Tests.Integration;

using Xunit;

[CollectionDefinition("Mongo")]
public class MongoCollection : ICollectionFixture<MongoFixture>;