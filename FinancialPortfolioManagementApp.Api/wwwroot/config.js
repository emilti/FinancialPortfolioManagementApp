const config = {
  development: {
    API_BASE_URL: "https://localhost:7276"
  },
  //production: {
  //  API_BASE_URL: "https://api.yourdomain.com"
  //}
};

export default config[process.env.NODE_ENV || 'development'];