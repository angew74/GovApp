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
        ], 'partito': [
            './wwwroot/js/app.js',
            './Views/Partito/Index.cshtml.js'
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
                    presets: ['es2017']
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