<template>
    <div id="app">
        <div id="header">
            <nav class="navbar navbar-icon-top navbar-expand-lg navbar-dark bg-dark">
                <a class="navbar-brand" href="http://Deltasi.it">
                    <img src="../css/images/goverment.png" style="height: 4em; display: inline-block;vertical-align: middle; max-width: 100%;" />
                </a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false"
                        aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item active">
                            <a class="nav-link" asp-controller="Home" asp-action="Index">
                                <i class="fa fa-home"></i>
                                <div>
                                    GovApp
                                </div>
                            </a>
                        </li>
                        <li class="navbar-nav active">
                            <a class="nav-link">
                                <i class="fa fa-sign-out-alt"></i>
                                <span class="badge badge-capitolium badge-pill"></span>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="sidebar">
                    <div class="sidebar-backdrop" @click="closeSidebarPanel" v-if="isPanelOpen"></div>
                    <transition name="slide">
                        <div v-if="isPanelOpen"
                             class="sidebar-panel">
                            <slot></slot>
                        </div>
                    </transition>
                </div>
                <Burger></Burger>
            </nav>
        </div>
        <Sidebar>
            <ul class="sidebar-panel-nav">              
                <li class="active">
                    <a asp-controller="Account" asp-action="Index">
                        <i class="fa fa-user"></i>
                        Profilo
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Affluenze" asp-action="Index">
                        <i class="fa fa-bars"></i>
                        Andamento
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Scrutinio" asp-action="Index">
                        <i class="fa fa-tasks"></i>
                        Scrutinio
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Interrogazioni" asp-action="Index">
                        <i class="fa fa-history"></i>
                        Interrogazioni
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Ricalcoli" asp-action="Index">
                        <i class="fa fa-calculator"></i>
                        Ricalcoli
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Controlli" asp-action="Index">
                        <i class="fa fa-receipt"></i>
                        Controlli
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Amministrazione" asp-action="Index">
                        <i class="fa fa-clone"></i>
                        Amministrazione
                    </a>
                </li>
                <li class="active">
                    <a asp-controller="Home" asp-action="Manuale">
                        <i class="fa fa-university"></i>
                        Manuali utente
                    </a>
                </li>
            </ul>
        </Sidebar>
    </div>
</template>
<script>
    import Burger from './components/Menu/Burger.vue';
    import Sidebar from './components/Menu/Sidebar.vue';

    export default {
        name: 'app',
        components: {
            Burger,
            Sidebar
        }
    }
</script>
<style>
    html {
        height: 100%;
        overflow: hidden;
    }

    body {
        border: 0;
        margin: 0;
        padding: 0;
        font-family: 'Lato';
        height: 100%;
        /* background: rgb(101,31,87);
        background: linear-gradient(45deg, rgba(101,31,87,1) 0%, rgba(225,113,87,1) 48%, rgba(249,248,113,1) 100%);*/
    }

    .bg-dark {
    background-color: #54272F !important;
}


    .main-nav {
        display: flex;
        justify-content: space-between;
        padding: 0.5rem 0.8rem;
    }

    ul.sidebar-panel-nav {
        list-style-type: none;
    }

        ul.sidebar-panel-nav > li.active > a {
            color: white;
            text-decoration: none;
            font-size: 1.5rem;
            display: block;
            padding-bottom: 0.5em;
        }
</style>
