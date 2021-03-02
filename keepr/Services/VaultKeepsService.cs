using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
	public class VaultKeepsService
	{

		private readonly VaultKeepsRepository _repo;
		private readonly VaultsRepository _vrepo;
		private readonly KeepsRepository _krepo;
		public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo, KeepsRepository krepo)
		{
			_repo = repo;
			_vrepo = vrepo;
			_krepo = krepo;
		}

		internal void Create(VaultKeep vaultKeep, string id, int vaultId, int keepId)
		{
			Vault vault = _vrepo.GetById(vaultId);
			Keep keep = _krepo.GetById(keepId);
			if(vault == null || keep == null)
			{
				throw new Exception ("Invalid keep/vault Id");
			}
			if(vault.CreatorId != id || keep.CreatorId != id)
			{
				throw new Exception("Not the Owner of this object");
			}
			_repo.Create(vaultKeep);
		}

		internal void Delete(int id)
		{
			var data = _repo.GetById(id);
			if (data == null)
			{
				throw new Exception("Invalid Id");
			}
			_repo.Delete(id);
		}
	}
}