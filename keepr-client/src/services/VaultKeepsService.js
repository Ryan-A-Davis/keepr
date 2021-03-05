// import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class VaultKeepsService {
  async create(keepId, vaultId) {
    const res = await api.post('api/vaultkeeps', { vaultId: vaultId, keepId: keepId })
    logger.log(res.data)
  }

  async delete(keepId, vaultId) {
    const res = await api.delete('api/vaultkeeps', { vaultId: vaultId, keepId: keepId })
    logger.log(res)
  }
}
export const vaultKeepsService = new VaultKeepsService()
