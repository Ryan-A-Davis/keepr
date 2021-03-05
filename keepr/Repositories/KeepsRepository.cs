using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
	public class KeepsRepository
	{

		private readonly IDbConnection _db;

		public KeepsRepository(IDbConnection db)
		{
			_db = db;
		}
		internal IEnumerable<Keep> GetAll()
		{
			string sql = @"
			SELECT
			keep.*,
			prof.*
			FROM keeps keep
			JOIN profiles prof ON keep.creatorId = prof.id";
			return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; });
		}

		internal Keep GetById(int id)
		{
			string sql = @"
			SELECT 
			keep.*,
			pr.*
			FROM keeps keep
			JOIN profiles pr ON keep.creatorId = pr.id
			WHERE keep.id = @id;";
			return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
			{
				keep.Creator = profile;
				return keep;
			}, new { id }, splitOn: "id").FirstOrDefault();
		}

		internal IEnumerable<KeepVaultKeepViewModel> GetByVaultId(int id)
		{
			string sql = @"
			SELECT
			keep.*,
			vk.id AS VaultKeepId,
			prof.*		
			FROM vaultkeeps vk
			JOIN keeps keep ON vk.keepId = keep.id
			JOIN profiles prof ON keep.creatorId = prof.id
			WHERE vaultId = @id;";
			IEnumerable<KeepVaultKeepViewModel> keeps = _db.Query<KeepVaultKeepViewModel, Profile, KeepVaultKeepViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
			return keeps;
		}

		internal int Create(Keep newKeep)
		{
			string sql = @"
			INSERT INTO keeps
			(creatorId, name, img, description, views, keeps)
			VALUES
			(@CreatorId, @Name, @Img, @Description, @Views, @Keeps);
			SELECT LAST_INSERT_ID();";
			return _db.ExecuteScalar<int>(sql, newKeep);
		}

		internal IEnumerable<Keep> GetAllByUser(string id)
		{
			string sql = @"
			SELECT 
			keep.*,
			prof.*
			FROM keeps keep
			JOIN profiles prof ON keep.creatorId = prof.id
			WHERE keep.creatorId = @id;";
			return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
			_db.Execute(sql, new { id });
		}

		internal Keep Edit(Keep updated)
		{
			string sql = @"
        UPDATE keeps
        SET
        name = @Name,
				description = @Description,
				views = @Views,
				keeps = @Keeps
        WHERE id = @Id;";
			_db.Execute(sql, updated);
			return updated;
		}
	}
}