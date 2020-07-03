import axios from 'axios'

class Auth {

  constructor() {
    let LOGIN_URL = 'http://localhost:5000/api/account';
    this.http = axios.create({
      baseURL: LOGIN_URL
    })
    this.user = JSON.parse(localStorage.getItem('user'));
  }

  // Send a request to the login URL and save the returned JWT
  login(creds) {
    return this.http.post('login', creds)
      .then((response) => {
        let data = response.data;

        this.user = {};
        this.user.isAuthenticated = true;
        this.user.isAdmin = data.role === 'admin';
        this.user.email = data.email;

        localStorage.setItem('user', JSON.stringify(this.user));
        localStorage.setItem('access_token', data.token);

        return data;
      });
  }

  register(creds) {
    return this.http.post('register', creds)
      .then(() => {
        return this.login(creds);
      });
  }

  isAdmin() {
    return this.user && this.user.isAdmin;
  }

  isLoggedIn() {
    return this.user !== null;
  }

  getEmail() {
    return this.user ? this.user.email : ' ';
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('access_token');

    this.user = null;
  }
}

let AuthService = new Auth();

export default AuthService;
