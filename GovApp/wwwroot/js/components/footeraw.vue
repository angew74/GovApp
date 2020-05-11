<template>
    <section class="absolute" id="footer">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 mt-2 mt-sm-5">
                    <ul class="list-unstyled list-inline social text-center">
                        <li class="list-inline-item"><a href="javascript:void();"><font-awesome-icon :icon="{ prefix: 'fab', iconName: 'twitter' }" /></a></li>
                        <li class="list-inline-item"><a href="javascript:void();"><font-awesome-icon :icon="{ prefix: 'fab', iconName: 'medium' }" /></a></li>
                        <li class="list-inline-item"><a href="javascript:void();"><font-awesome-icon :icon="{ prefix: 'fab', iconName: 'github' }" /></a></li>
                        <li class="list-inline-item"><a href="javascript:void();"><font-awesome-icon :icon="{ prefix: 'fab', iconName: 'blogger' }" /></a></li>         
                        <li class="list-inline-item"><a href="javascript:void();" target="_blank"><v-icon name="envelope" /></a></li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 mt-2 mt-sm-2 text-center text-white">
                    <p class="h6">&copy Tutti i diritti riservati.<a class="text-white ml-2" href="" target="_blank">N.R. Creations</a> powered by <font-awesome-icon :icon="{ prefix: 'fab', iconName: 'microsoft' }" /><font-awesome-icon :icon="{ prefix: 'fab', iconName: 'vuejs' }" /></p>
                </div>
            </div>
        </div>
    </section>
    </template>
<script>
  export default {
    name: 'footeraw'
  }
</script>
