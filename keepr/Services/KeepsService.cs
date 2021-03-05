using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using keepr.Exceptions;

namespace keepr.Services
{
	public class KeepsService
	{

		private readonly KeepsRepository _repo;
		private readonly VaultsRepository _vrepo;
		private readonly ProfilesRepository _prepo;
		public KeepsService(KeepsRepository repo, VaultsRepository vrepo, ProfilesRepository prepo)
		{
			_repo = repo;
			_vrepo = vrepo;
			_prepo = prepo;
		}
		public IEnumerable<Keep> GetAll()
		{
			IEnumerable<Keep> keeps = _repo.GetAll();
			return keeps;
		}

		public Keep GetById(int id)
		{
			Keep original = _repo.GetById(id);
			if (original == null) { throw new Exception("Invalid Id"); }
			return original;
		}

		internal IEnumerable<KeepVaultKeepViewModel> GetByVaultId(int id, string userId)
		{
			Profile profile = _prepo.GetById(userId);
			Vault vault = _vrepo.Get(id);
			if (vault == null)
			{
				throw new Exception("Invalid Id");
			}
			if (vault.IsPrivate)
			{
				throw new NotAuthorized("This vault has been set to private!");
			}
			return _repo.GetByVaultId(id);
		}

		public Keep Create(Keep newKeep)
		{
			newKeep.Id = _repo.Create(newKeep);
			return newKeep;
		}

		internal Keep Edit(Keep editData, string userId)
		{
			Keep original = GetById(editData.Id);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, cannot edit a keep that isn't yours");
			}
			editData.Name = editData.Name == null ? original.Name : editData.Name;
			editData.Img = editData.Img == null ? original.Img : editData.Img;
			editData.Description = editData.Description == null ? original.Description : editData.Description;
			editData.Views = editData.Views == 0 ? original.Views : editData.Views;
			editData.Keeps = editData.Keeps == 0 ? original.Keeps : editData.Keeps;
			return _repo.Edit(editData);
		}

		internal IEnumerable<Keep> GetKeepsByUser(string id)
		{
			IEnumerable<Keep> keeps = _repo.GetAllByUser(id);
			return keeps;
		}

		internal string Delete(int id, string userId)
		{
			Keep original = GetById(id);
			if (original.CreatorId != userId)
			{
				throw new Exception("Access Denied, you may not delete something that doesn't belong to you");
			}
			_repo.Delete(id);
			return "deleted";
		}
	}
}