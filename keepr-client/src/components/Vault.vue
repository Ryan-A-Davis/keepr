<template>
  <div class="vault col-3">
    <div class="card rounded drop-shadow">
      <div class="card-body">
        <h4 class="card-title" @click="goToVault(vaultProps.id)">
          {{ vaultProps.name }}
        </h4>
        <button class="btn btn-danger" v-if="vaultProps.creatorId === state.account.id" @click="delete(vaultProps.id)">
          Delete
        </button>
      </div>
    </div>
    <VaultEditModal :vault-props="vaultProps" />
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import NotificationService from '../services/NotificationService'
import { logger } from '../utils/Logger'

export default {
  name: 'Vault',
  props: {
    vaultProps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      goToVault(id) {
        router.push({ name: 'VaultDetails', params: { id: id } })
      },
      async delete(id) {
        try {
          if (await NotificationService.confirm()) {
            await vaultsService.delete(id)
          } else {
            alert('changes not saved')
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async edit(update, id) {
        try {
          if (await NotificationService.confirm()) {
            await vaultsService.edit(update, id)
          } else {
            alert('Changes will be discarded')
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.card{
  background-image: url("https://image.shutterstock.com/z/stock-photo-front-view-of-light-silver-bank-vault-door-closed-d-render-386619208.jpg") ;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;

  min-height: 200px;
  min-width: 200px;
}

</style>
