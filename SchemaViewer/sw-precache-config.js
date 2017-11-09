module.exports = {
    //staticFileGlobs: [
    //    'dist/**.html',
    //],
    runtimeCaching: [{
        urlPattern: /api\/Calendar/, // change to real url later
        handler: 'networkFirst'
    }],
    root: 'wwwroot'
};