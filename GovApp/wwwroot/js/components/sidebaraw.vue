<template>
    <div>
        <b-navbar toggleable="lg" type="dark" variant="dark">
            <b-navbar-brand href="http://Deltasi.it">
                <img src="../../css/images/goverment.png" style="height: 4em; display: inline-block;vertical-align: middle; max-width: 100%;" />
            </b-navbar-brand>
            <b-iconstack font-scale="3" rotate="90" style="cursor: pointer;" v-b-toggle.sidebar-1>
                <b-icon stacked icon="chevron-right" shift-h="-4" variant="danger"></b-icon>
                <b-icon stacked icon="chevron-right" shift-h="0" variant="success"></b-icon>
                <b-icon stacked icon="chevron-right" shift-h="4" variant="primary"></b-icon>
            </b-iconstack>
           <!-- <b-button size="lg" variant="dark" class="mb-2" v-b-toggle.sidebar-1><v-icon name="map" scale="3" label="Menu" /></b-button>  -->         
            <b-nav-item href="home/index" active>
                <b-iconstack font-scale="5" animation="spin">
                    <b-icon stacked icon="house" variant="info" scale="0.65" shift-v="-0.25"></b-icon>                
                </b-iconstack>
                </b-nav-item>
        </b-navbar>
                <b-sidebar id="sidebar-1" bg-variant="dark" text-variant="light" shadow>
                    <div class="px-3 py-2">
                        <b-navbar-nav class="ml-auto">
                            <b-nav-item active href="/partito/index">
                                    <v-icon name="user-secret" />
                                    Premier
                            </b-nav-item>
                            <b-nav-item active href="#">
                                    <v-icon name="bars" />
                                    Andamento
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="tasks" />
                                    Scrutinio
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="history" />
                                    Interrogazioni
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="calculator" />
                                    Ricalcoli
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="receipt" />
                                    Controlli
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="clone" />
                                    Amministrazione
                            </b-nav-item>
                            <b-nav-item active>
                                    <v-icon name="university" />
                                    <i class="fa fa-university"></i>
                                    Manuali utente
                            </b-nav-item>
                        </b-navbar-nav>
                    </div>
                </b-sidebar>
    </div>
       </template>
<script>
  export default {
    name: 'sidebaraw'    
  }
</script>
