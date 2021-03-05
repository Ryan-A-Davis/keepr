<template>
  <div class="VaultCreateModal">
    <div class="modal fade"
         id="vaultCreateModal"
         data-backdrop="static"
         data-keyboard="false"
         tabindex="-1"
         aria-labelledby="staticBackdropLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="staticBackdropLabel">
              Edit Vault
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault()" class="form-group">
              <input type="text" v-model="state.newVault.name" placeholder="Enter vault name" required>
              <input type="text" v-model="state.newVault.description" placeholder="Enter vault description?" required>
              <input type="checkbox" v-model="state.newVault.isPrivate"> <p>Make Private?</p>
              <button type="submit" class="btn btn-primary">
                Save Changes
              </button>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
export default {
  name: 'VaultCreateModal',
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      newVault: {},
      newKeep: {}
    })
    return {
      state,
      async createVault() {
        state.newVault.creatorId = state.account.id
        try {
          await vaultsService.create(state.newVault)
        } catch (error) {
          logger.error(error)
        }
        state.newVault = {}
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
