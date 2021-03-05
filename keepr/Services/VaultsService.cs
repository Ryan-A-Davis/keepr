using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Exceptions;
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
			return _repo.GetVaultsByUser(id).ToList().FindAll(v => !v.IsPrivate);
		}

		public Vault Get(int id, string userId)
		{
			Vault vault = _repo.Get(id);
			if (vault == null)
			{
				throw new Exception("Invalid Id");
			}
			if (vault.CreatorId != userId && vault.IsPrivate)
			{
				throw new NotAuthorized("You are not the owner, and this vault is set to private");
			}
			return vault;
		}

		public Vault Create(Vault newVault)
		{
			newVault.Id = _repo.Create(newVault);
			return newVault;
		}


		internal Vault Edit(Vault editData, string userId)
		{
			Vault original = Get(editData.Id, userId);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, cannot edit a Vault that isn't yours");
			}
			editData.Name = editData.Name == null ? original.Name : editData.Name;
			editData.Description = editData.Description == null ? original.Description : editData.Description;
			editData.IsPrivate = editData.IsPrivate == true ? editData.IsPrivate : original.IsPrivate;
			return _repo.Edit(editData);
		}

		internal string Delete(int id, string userId)
		{
			Vault original = Get(id, userId);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, you may not delete something that doesn't belong to you");
			}
			_repo.Delete(id);
			return "deleted";
		}
	}
}