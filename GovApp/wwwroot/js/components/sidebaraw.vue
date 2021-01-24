<template>
    <div>
        <b-navbar toggleable="lg" type="dark" variant="dark">            
            <b-nav-item v-if="isAuthenticated" style="margin-top:28px">
                <b-iconstack id="menu-toogle" font-scale="3" rotate="90" style="cursor: pointer;" v-b-toggle.sidebar-1>
                    <b-icon stacked icon="chevron-right" shift-h="-4" variant="danger"></b-icon>
                    <b-icon stacked icon="chevron-right" shift-h="0" variant="success"></b-icon>
                    <b-icon stacked icon="chevron-right" shift-h="4" variant="primary"></b-icon>
                </b-iconstack>
                <b-tooltip target="menu-toogle" triggers="hover">
                    apri il menu
                </b-tooltip>
            </b-nav-item>
            <b-nav-item  v-if="!isAuthenticated" href="/GovApp/home/index" active>
                <b-iconstack id="home-icon" font-scale="5" animation="spin">
                    <b-icon stacked icon="house" variant="info" scale="0.65" shift-v="-0.25"></b-icon>
                </b-iconstack>
                <b-tooltip target="home-icon" triggers="hover">
                    vai all'homepage autenticandoti
                </b-tooltip>
            </b-nav-item>
            <b-nav-item v-if="isAuthenticated" href="/GovApp/account/logout" active>
                <b-iconstack id="logout-icon" font-scale="1.7">
                    <b-icon icon="power" class="rounded-circle bg-danger p-2" variant="light"></b-icon>
                </b-iconstack>
                <b-tooltip target="logout-icon" triggers="hover">
                    esci dall'applicazione
                </b-tooltip>
            </b-nav-item>
            <b-nav-item v-if="isAuthenticated" href="/GovApp/account/myprofile" active>
                <b-iconstack id="user-icon" font-scale="1.7">
                    <b-icon icon="person-fill" class="rounded-circle bg-danger p-2" variant="light"></b-icon>
                </b-iconstack>
                <b-tooltip target="user-icon" triggers="hover">
                    vedi il tuo profilo {{ profile.name }}
                </b-tooltip>
            </b-nav-item>
            <b-nav-item v-if="!isAuthenticated">
                <b-button id="login-button" v-b-modal.prevent.loginModal style="background-color:#343a40;border:none" variant="dark" class="mb-2" size="xl" type="submit">
                    <b-icon icon="person-fill" aria-label="Help"></b-icon><br />Accedi
                </b-button>
                <b-tooltip target="login-button" triggers="hover">
                    accedi all'applicazione
                </b-tooltip>
            </b-nav-item>
        </b-navbar>
        <div v-if="isAuthenticated">
            <b-sidebar v-if="voci.length" id="sidebar-1" bg-variant="dark" shadow>
                <div class="px-3 py-2">
                    <div v-bind:key="data.index" v-for="(data,index) in voci">
                        <b-navbar-nav class="ml-auto">
                            <b-nav-item v-bind:active="data.active" link-classes="nav-link-menu" v-bind:href="data.link">
                                <v-icon v-bind:name="data.icon" />
                                {{data.descrizione}}
                            </b-nav-item>
                        </b-navbar-nav>
                    </div>
                </div>
            </b-sidebar>
        </div>
        <login-modal></login-modal>
    </div>  
</template>
<script>
    import { mapGetters, mapState, mapActions } from 'vuex';
    import loginaw from './loginaw.vue';
    export default {
        name: 'sidebaraw',
        namespaced: true,
        components: {
            'login-modal': loginaw
        },
        computed: {
            ...mapState('context', [
                'profile'
            ]),
            ...mapGetters('context', [
                'isAuthenticated'
            ])
        },
        methods: {
            ...mapActions('context', [
                'logout'
            ])
        },
        data: function () {
            return { voci: [] }
        },
        mounted() {
            axios({
                method: 'get',
                url: '/GovApp/values/menu'
            })
                .then(response => {
                    this.voci = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }
</script>
