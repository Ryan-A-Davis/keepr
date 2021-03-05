import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    console.log(res)
    AppState.vaults = res.data
  }

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    console.log(res)
    AppState.activeVault = res.data
  }

  async create(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    console.log(res)
    AppState.vaults = [...AppState.vaults, res.data]
  }

  async edit(vaultId, edited) {
    const toUpdate = edited
    const res = await api.put('api/vaults/' + vaultId, toUpdate)
    const vaultInd = AppState.vaults.findIndex(k => k.id === vaultId)
    AppState.vaults.splice(vaultInd, 1, res.data)
  }

  async delete(vaultId) {
    await api.delete('api/vaults/' + vaultId)
    const vaultInd = AppState.vaults.findIndex(k => k.id === vaultId)
    AppState.vaults.splice(vaultInd, 1)
  }

  async addKeep(id) {
    const res = await api.post('api/vaults/' + id + '/keeps')
    console.log(res.data)
  }
}

export const vaultsService = new VaultsService()
