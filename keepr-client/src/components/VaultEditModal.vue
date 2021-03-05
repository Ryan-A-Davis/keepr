<template>
  <div class="VaultEditModal">
    <div class="modal fade"
         :id="'staticBackdrop' + vaultProps.id"
         data-backdrop="static"
         data-keyboard="false"
         tabindex="-1"
         aria-labelledby="staticBackdropLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog">
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
            <form @submit.prevent="edit(vaultProps.id)" class="form-group">
              <input type="text" v-model="state.updated.name" placeholder="Change vault name?" required>
              <input type="text" v-model="state.updated.description" placeholder="Change vault description?" required>
              <input type="checkbox" v-model="state.updated.isPrivate">
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button type="submit" class="btn btn-primary">
              Save Changes
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
export default {
  name: 'VaultEditModal',
  props: {
    vaultProps: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      updated: {}
    })
    return {
      state,
      async edit(id) {
        try {
          await vaultsService.edit(id)
        } catch (error) {

        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
