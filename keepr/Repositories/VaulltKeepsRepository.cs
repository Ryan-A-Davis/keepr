using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
	public class VaultKeepsRepository
	{

		private readonly IDbConnection _db;

		public VaultKeepsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal void Create(VaultKeep vk)
		{
			string sql = @"
			INSERT INTO vaultkeeps
			(vaultId, keepId)
			VALUES
			(@VaultId, @KeepId);";
			_db.Execute(sql, vk);
		}

		internal VaultKeep GetById(int id)
		{
			string sql = "SELECT FROM vaultkeeps WHERE id = @id;";
			return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
			_db.Execute(sql, new { id });
		}
	}
}