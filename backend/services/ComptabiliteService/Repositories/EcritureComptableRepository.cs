﻿using ComptabiliteService.Entities;
using ComptabiliteService.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ComptabiliteService.Repositories
{
    public class EcritureComptableRepository : IEcritureComptableRepository
    {
        private readonly IMongoCollection<EcritureComptable> ecritureComptableCollection;
        public EcritureComptableRepository(IMongoDatabase database)
        {
            this.ecritureComptableCollection = database.GetCollection<EcritureComptable>("EcritureComptables");
        }
        public async Task AddEcritureComptable(EcritureComptable ecritureComptable)
        {
            await ecritureComptableCollection.InsertOneAsync(ecritureComptable).ConfigureAwait(false);
        }

        public async Task DeleteEcritureComptable(ObjectId id)
        {
            await ecritureComptableCollection.DeleteOneAsync(ec => ec.Id == id).ConfigureAwait(false);
        }

        public async Task<List<EcritureComptable>> GetAllEcrituresComptable()
        {
            return await ecritureComptableCollection.Find(ec => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<EcritureComptable> GetEcritureComptableById(ObjectId Id)
        {
            return await ecritureComptableCollection.Find(ec => ec.Id == Id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task UpdateEcritureComptable(ObjectId id, EcritureComptable ecritureComptable)
        {
            await ecritureComptableCollection.ReplaceOneAsync(ec => ec.Id == id, ecritureComptable).ConfigureAwait(false);
        }
    }
}