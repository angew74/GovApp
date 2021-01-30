/// <binding Clean='Run - Development' />
"use strict";
const { VueLoaderPlugin } = require('vue-loader');
const path = require('path');
const webpack = require('webpack');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

module.exports = {
    entry: {
        'home': [
            './wwwroot/js/app.js',
            './Views/Home/Index.cshtml.js'
        ],
        'andamento': [
            './wwwroot/js/app.js',
            './Views/Andamento/Index.cshtml.js'
        ],
        'coalizioni': [
            './wwwroot/js/app.js',
            './Views/Coalizioni/Index.cshtml.js'
        ],
        'insandamento': [
            './wwwroot/js/app.js',
            './Views/Andamento/Inserimento.cshtml.js'
        ],
        'modandamento': [
            './wwwroot/js/app.js',
            './Views/Andamento/Modifica.cshtml.js'
        ],
        'inscoalizioni': [
            './wwwroot/js/app.js',
            './Views/Coalizioni/Inserimento.cshtml.js'
        ],
        'modcoalizioni': [
            './wwwroot/js/app.js',
            './Views/Coalizioni/Modifica.cshtml.js'
        ],
        'login': [
            './wwwroot/js/app.js',
            './Views/Account/Login.cshtml.js'
        ],
        'confirmemail': [
            './wwwroot/js/app.js',
            './Views/Account/ConfirmEmail.cshtml.js'
        ],
        'accessdenied': [
            './wwwroot/js/app.js',
            './Views/Account/AccessDenied.cshtml.js'
        ],
        'logoutconfirmation': [
            './wwwroot/js/app.js',
            './Views/Account/logoutconfirmation.cshtml.js'
        ],
        'account': [
            './wwwroot/js/app.js',
            './Views/Account/index.cshtml.js'
        ],
        'confirmemailconfirmation': [
            './wwwroot/js/app.js',
            './Views/Account/confirmemailconfirmation.cshtml.js'
        ],
        'changepassword': [
            './wwwroot/js/app.js',
            './Views/Account/changepassword.cshtml.js'
        ],
        'confirmpassword': [
            './wwwroot/js/app.js',
            './Views/Account/changepasswordconfirm.cshtml.js'
        ],
        'rights': [
            './wwwroot/js/app.js',
            './Views/Rights/index.cshtml.js'
        ],
        'manage': [
            './wwwroot/js/app.js',
            './Views/Account/manage.cshtml.js'
        ],
        'register': [
            './wwwroot/js/app.js',
            './Views/Account/register.cshtml.js'
        ],
        'sezioni': [
            './wwwroot/js/app.js',
            './Views/Sezioni/index.cshtml.js'
        ],
        'status': [
            './wwwroot/js/app.js',
            './Views/Sezioni/status.cshtml.js'
        ],
        'myprofile': [
            './wwwroot/js/app.js',
            './Views/Account/MyProfile.cshtml.js'
        ],
        'interrogazioni': [
            './wwwroot/js/app.js',
            './Views/Interrogazioni/Index.cshtml.js'
        ],
        'ricalcoli': [
            './wwwroot/js/app.js',
            './Views/Ricalcoli/Index.cshtml.js'
        ],
        'liste': [
            './wwwroot/js/app.js',
            './Views/Interrogazioni/Liste.cshtml.js'
        ],
        'rliste': [
            './wwwroot/js/app.js',
            './Views/Ricalcoli/Liste.cshtml.js'
        ],
        'rsindaco': [
            './wwwroot/js/app.js',
            './Views/Ricalcoli/Sindaco.cshtml.js'
        ]

    },
    output: {      
        filename: '[name].bundle.js',
        path: path.join(__dirname, '/wwwroot/dest/js/'),
        publicPath: "/dest/js/"     
    },
    plugins: [
        new webpack.ProvidePlugin({
            Popper: ['popper.js', 'default'],
            axios: 'axios'
        }),
        new VueLoaderPlugin()
    ],

    optimization: {
        minimizer: [
            new UglifyJsPlugin({
                cache: true,
                parallel: true,
                uglifyOptions: {
                    compress: false,
                    ecma: 6,
                    mangle: true
                },
                sourceMap: true
            })
        ]
    },
   module: {
        rules: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /(node_modules)/,
                options: {
                    presets: [
                        ['es2017']
                    ],
                    plugins: [
                        ['transform-runtime'],
                        ['transform-object-rest-spread'],
                        ['transform-es2015-destructuring']
                    ]
                }
            },
           {
               test: /\.css$/,
               use: [{
                   loader: 'style-loader',
               }, {
                   loader: 'css-loader'
               }]
           },
           {
               test: /\.scss$/,
               use: [{
                   loader: "style-loader"
               }, {
                   loader: "css-loader"
               }, {
                   loader: "sass-loader",
                   options: {
                   }
               }]
           },
           {
               test: /\.(svg|eot|woff|woff2|ttf)$/,
               use: ['file-loader']
           },
           {
               test: /\.(png|jpg|gif)$/,
               use: {
                   loader: 'url-loader',
                   options: {
                       limit: 8192
                   }
               }
           },
           {
               test: /\.vue$/,
               loader: 'vue-loader'
           }
        ]
    },
    resolve: {
        alias: {
            vue: 'vue/dist/vue.js',
            'bootstrap-vue$': 'bootstrap-vue/src/index.js'
        },
        extensions: ['.js', '.vue']
    }
};