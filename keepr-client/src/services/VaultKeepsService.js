// import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class VaultKeepsService {
  async create(vaultId, keepId) {
    const res = await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
    logger.log(res.data)
  }

  async delete(id) {
    const res = await api.delete('api/vaultkeeps/' + id)
    logger.log(res)
  }
}
export const vaultKeepsService = new VaultKeepsService()
