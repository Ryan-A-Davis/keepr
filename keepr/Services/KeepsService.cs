using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
	public class KeepsService
	{

		private readonly KeepsRepository _repo;
		public KeepsService(KeepsRepository repo)
		{
			_repo = repo;
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

		internal IEnumerable<Keep> GetByVaultId(int id)
		{
			return _repo.GetByVaultId(id)
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