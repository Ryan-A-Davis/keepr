<template>
  <div class="KeepCreateModal">
    <div class="modal fade"
         id="keepCreateModal"
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
              Create Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createKeep()" class="form-group">
              <input type="text" v-model="state.newKeep.name" placeholder="Enter eep name..." required>
              <input type="text" v-model="state.newKeep.description" placeholder="Enter keep description..." required>
              <input type="text" v-model="state.newKeep.img" placeholder="Enter an img url..." required>
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
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
export default {
  name: 'KeepCreateModal',
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        state.newKeep.creatorId = state.account.id
        try {
          await keepsService.create(state.newKeep)
        } catch (error) {
          logger.error(error)
        }
        state.newKeep = {}
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
