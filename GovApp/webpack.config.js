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
        'affluenze': [
            './wwwroot/js/app.js',
            './Views/Affluenze/Index.cshtml.js'
        ],
        'insaffluenze': [
            './wwwroot/js/app.js',
            './Views/Affluenze/Inserimento.cshtml.js'
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
                query: {
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
                loaders: ['style-loader', 'css-loader']
            },
            {
                test: /\.scss$/,
                loaders: ['style-loader', 'sass-loader']
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