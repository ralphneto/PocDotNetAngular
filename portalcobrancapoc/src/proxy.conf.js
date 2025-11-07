const PROXY_CONFIG = [
  {
    context: [
      '/api',
    ],
    target: 'http://localhost:5191',
    secure: false,
    changeOrigin: true,
    pathRewrite: {
      "^/": ""
    }
  }
]

module.exports = PROXY_CONFIG;
