import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getVaults() {
    const res = await api.get('api/vaults')
    console.log(res)
    AppState.vaults = res.data
  }

  async getvault(id) {
    const res = await api.get('api/vaults/' + id)
    console.log(res)
    AppState.activevault = res.data
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    console.log(res)
  }

  async editVault(vaultId, edited) {
    const toUpdate = edited
    const res = await api.put('api/vaults/' + vaultId, toUpdate)
    const vaultInd = AppState.vaults.findIndex(k => k.id === vaultId)
    AppState.vaults.splice(vaultInd, 1, res.data)
  }

  async deleteVault(vaultId) {
    await api.delete('api/vaults/' + vaultId)
    const vaultInd = AppState.vaults.findIndex(k => k.id === vaultId)
    AppState.vaults.splice(vaultInd, 1)
  }

  async addKeep(id) {
    await api.post('api/vaults/' + id + '/keeps')
  }
}

export const vaultsService = new VaultsService()
