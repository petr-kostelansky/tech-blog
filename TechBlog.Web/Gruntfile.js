/// <binding BeforeBuild='build' ProjectOpened='watch' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        properties: grunt.file.readJSON('App_Data/BowerBundles.json'),

        sass: {
            dist: {
                files: [{
                    expand: true,
                    cwd: 'resources/sass/',
                    src: ['**/*.scss'],
                    dest: 'resources/sass/css',
                    ext: '.css'
                }]
            }
        },

        concat: {
            options: {
                sourcemap: true
            },
            //This is the standard way of concatenating files using setting.json
            // - scr:  takes an array by accessing a json property, in this case mainCss, from the setting.json file
            // - dest: to use BunndleForBower.cs you need to save the file into a set directory and with the same name as the property
            css: {
                src: new Array('<%= properties.mainCss %>'),
                dest: 'Content/css/mainCss.css'
            },
            articleCss: {
                src: new Array('<%= properties.articleCss %>'),
                dest: 'Content/css/articleCss.css'
            },
            articleJs: {
                src: new Array('<%= properties.articleJs %>'),
                dest: 'Content/js/articleJs.js'
            },
            mainJs: {
                src: new Array('<%= properties.mainJs %>'),
                dest: 'Content/js/mainJs.js'
            },
            headAppJs: {
                src: new Array('<%= properties.headAppJs %>'),
                dest: 'Content/js/headAppJs.js'
            }, 
            appJs: {
                src: new Array('<%= properties.appJs %>'),
                dest: 'Content/js/appJs.js'
            }, 
            jqueryUiCss: {
                src: new Array('<%= properties.jqueryUiCss %>'),
                dest: 'Content/css/jqueryUiCss.css'
            },
            jqueryUi: {
                src: new Array('<%= properties.jqueryUi %>'),
                dest: 'Content/js/jqueryUi.js'
            },
            jqueryval: {
                src: new Array('<%= properties.jqueryval %>'),
                dest: 'Content/js/jqueryval.js'
            },
            tokenfieldJs: {
                src: new Array('<%= properties.tokenfieldJs %>'),
                dest: 'Content/js/tokenfieldJs.js'
            },
            tokenfieldCss: {
                src: new Array('<%= properties.tokenfieldCss %>'),
                dest: 'Content/css/tokenfieldCss.css'
            },
            dataTableJs: {
                src: new Array('<%= properties.dataTableJs %>'),
                dest: 'Content/js/dataTableJs.js'
            },
            dataTableCss: {
                src: new Array('<%= properties.dataTableCss %>'),
                dest: 'Content/css/dataTableCss.css'
            },
        },

        cssmin: {
            css: {
                src: 'Content/css/mainCss.css',
                dest: 'Content/css/mainCss.min.css'
            },
            articleCss: {
                src: 'Content/css/articleCss.css',
                dest: 'Content/css/articleCss.min.css'
            },
            tokenfieldCss: {
                src: 'Content/css/tokenfieldCss.css',
                dest: 'Content/css/tokenfieldCss.min.css'
            },
            jqueryUiCss: {
                src: 'Content/css/jqueryUiCss.css',
                dest: 'Content/css/jqueryUiCss.min.css'
            },
            dataTableCss: {
                src: 'Content/css/dataTableCss.css',
                dest: 'Content/css/dataTableCss.min.css'
            },
        },

        uglify: {
            options: {
                sourceMap: true,
                sourceMapIncludeSources: true
            },
            articleJs: {
                src: 'Content/js/articleJs.js',
                dest: 'Content/js/articleJs.min.js'
            },
            mainJs: {
                src: 'Content/js/mainJs.js',
                dest: 'Content/js/mainJs.min.js'
            },
            headAppJs: {
                src: 'Content/js/headAppJs.js',
                dest: 'Content/js/headAppJs.min.js'
            },
            appJs: {
                src: 'Content/js/appJs.js',
                dest: 'Content/js/appJs.min.js'
            },
            jqueryUi: {
                src: 'Content/js/jqueryUi.js',
                dest: 'Content/js/jqueryUi.min.js'
            },
            jqueryval: {
                src: 'Content/js/jqueryval.js',
                dest: 'Content/js/jqueryval.min.js'
            },
            tokenfieldJs: {
                src: 'Content/js/tokenfieldJs.js',
                dest: 'Content/js/tokenfieldJs.min.js'
            },
            dataTableJs: {
                src: 'Content/js/dataTableJs.js',
                dest: 'Content/js/dataTableJs.min.js'
            },
        }

        ,copy: {
            fonts: {
                expand: true,
                cwd: 'bower_components/font-awesome/fonts/',
                src: '*',
                flatten: true,
                dest: 'Content/fonts/'
            }
        }

        ,watch: {
            configFiles: {
                files: ['gruntfile.js'],
                options: {
                    reload: true
                }
            },
            //These watch various libraray and specific application directories for changes and then call the appropriate build commands
            css: {
                files: ['bower_components/**/*.css', 'resources/css/*.css', 'resources/sass/*.scss'],
                tasks: ['build:css'],
                options: {
                    spawn: false,
                },
            },
            js: {
                files: ['bower_components/**/*.js', 'resources/js/*.js'],
                tasks: ['build:js'],
                options: {
                    spawn: false,
                },
            }
        }
    });

    grunt.log.write('sdsd')

    //// Load the various plugin that we need
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');

    //// grunt.registerTask allows you to define 'macro' commands that call multiple commands
    grunt.registerTask('default', []);

    grunt.registerTask('build:copy', [
        'copy']);

    //// Build task(s).
    grunt.registerTask('build:css', [
        'sass',
        'concat:css', 'concat:articleCss', 'concat:tokenfieldCss', 'concat:jqueryUiCss',  'concat:dataTableCss',
        'cssmin:css', 'cssmin:articleCss', 'cssmin:tokenfieldCss', 'cssmin:jqueryUiCss', 'cssmin:dataTableCss']);

    //// Build task(s).
    grunt.registerTask('build:js', [
        'concat:articleJs', 'concat:mainJs', 'concat:headAppJs', 'concat:appJs', 'concat:jqueryUi', 'concat:jqueryval', 'concat:tokenfieldJs', 'concat:dataTableJs',
        'uglify:articleJs', 'uglify:mainJs', 'uglify:headAppJs', 'uglify:appJs', 'uglify:jqueryUi', 'uglify:jqueryval', 'uglify:tokenfieldJs', 'uglify:dataTableJs']);

    //// Build task(s).
    grunt.registerTask('build', ['build:css', 'build:js']);
};
