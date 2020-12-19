export default {
    proxy: {
      '/api': {
        target: 'http://localhost:5000',
        changeOrigin: true
      },
      '/ws/okhall': {
        target: 'http://localhost:5000',
        changeOrigin: true,
        ws: true
      }
    }
  }