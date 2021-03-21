<template>
  <div class="keep col-4-lg-3 card item"
       type="button"
       :data-target="'#modal' + keepProps.id "
       data-toggle="modal"
       :style="`background-image: url(${keepProps.img})`"
  >
    <div class="row align-self-end justify-content-between mb-1">
      <div class="col-4">
        <p class="font-weight-bold text-white">
          {{ keepProps.name }}
        </p>
      </div>
      <div class="col-4">
        <img id="profImg" :src="keepProps.creator.picture" alt="">
      </div>
      <KeepModal :keep-props="keepProps" />
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
// import { keepsService } from '../services/KeepsService'
// import { logger } from '../utils/Logger'
export default {
  name: 'Keep',
  props: {
    keepProps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      account: computed(() => AppState.account),
      targetKeep: computed(() => AppState.activeKeep),
      user: computed(() => AppState.user)
    })
    return {
      state
    }
  }
}
</script>

 <style lang="scss" scoped>

#profImg{
  border-radius: 50%;
  max-height: 30px;
}
//  <KeepModal :keep-props="keepProps" />
</style>
