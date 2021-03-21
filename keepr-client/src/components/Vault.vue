<template>
  <div class="vault col-3">
    <div class="card rounded drop-shadow">
      <div class="card-body">
        <h4 class="card-title" @click="goToVault(vaultProps.id)">
          {{ vaultProps.name }}
        </h4>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
// import { vaultsService } from '../services/VaultsService'
// import NotificationService from '../services/NotificationsService'
// import { logger } from '../utils/Logger'

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
.h4:hover{
  cursor: pointer;
}

</style>
