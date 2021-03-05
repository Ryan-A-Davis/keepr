using System;
using System.Collections.Generic;
using keepr.Exceptions;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

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
		internal VaultKeep Get(int id)
		{
			VaultKeep exists = _repo.GetById(id);
			if (exists == null)
			{
				throw new Exception("Invalid Id");
			}
			return exists;
		}
		internal IEnumerable<VaultKeep> Get()
		{
			IEnumerable<VaultKeep> vaultkeeps = _repo.GetAll();
			return vaultkeeps;
		}
		internal void Create(VaultKeep vaultKeep, string id)
		{
			Vault vault = _vrepo.Get(vaultKeep.VaultId);
			Keep keep = _krepo.GetById(vaultKeep.KeepId);
			keep.Keeps++;
			if (vault == null || keep == null)
			{
				throw new Exception("Invalid keep/vault Id");
			}
			if (vaultKeep.CreatorId != id)
			{
				throw new NotAuthorized("You are not the owner of this vault");
			}
			vaultKeep.Id = _repo.Create(vaultKeep);
		}


		internal void Delete(int id, string userId)
		{
			VaultKeep data = Get(id);
			if (data == null || data.CreatorId != userId)
			{
				throw new Exception("Invalid Id and/or you are not the creator.");
			}
			_repo.Delete(id);
		}
	}
}