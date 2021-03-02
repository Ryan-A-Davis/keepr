using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
	public class VaultsService
	{

		private readonly VaultsRepository _repo;
		public VaultsService(VaultsRepository repo)
		{
			_repo = repo;
		}
		public IEnumerable<Vault> GetVaultsByUser(string id)
		{
			IEnumerable<Vault> vaults = _repo.GetVaultsByUser(id);
			return vaults;
		}

		public Vault Get(int id)
		{
			Vault original = _repo.GetById(id);
			if (original == null) { throw new Exception("Invalid Id"); }
			return original;
		}

		public Vault Create(Vault newVault)
		{
			newVault.Id = _repo.Create(newVault);
			return newVault;
		}

		internal Vault Edit(Vault editData, string userId)
		{
			Vault original = Get(editData.Id);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, cannot edit a Vault that isn't yours");
			}
			editData.Name = editData.Name == null ? original.Name : editData.Name;
			editData.Description = editData.Description == null ? original.Description : editData.Description;
			return _repo.Edit(editData);
		}

		internal string Delete(int id, string userId)
		{
			Vault original = Get(id);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, you may not delete something that doesn't belong to you");
			}
			_repo.Delete(id);
			return "deleted";
		}
	}
}