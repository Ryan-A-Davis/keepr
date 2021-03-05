using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Repositories
{
	public class VaultKeepsRepository
	{

		private readonly IDbConnection _db;

		public VaultKeepsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal IEnumerable<VaultKeep> GetAll()
		{
			string sql = @"
      SELECT * FROM vaultkeeps";
			return _db.Query<VaultKeep>(sql);
		}


		internal int Create(VaultKeep vk)
		{
			string sql = @"
			INSERT INTO vaultkeeps
			(vaultId, keepId, creatorId)
			VALUES
			(@VaultId, @KeepId, @CreatorId);
			SELECT LAST_INSERT_ID()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 ;";
			return _db.ExecuteScalar<int>(sql, vk);
		}

		internal VaultKeep GetById(int id)
		{
			string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
			return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
			_db.Execute(sql, new { id });
		}

	}
}