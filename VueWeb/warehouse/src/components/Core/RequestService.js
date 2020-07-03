import axios from 'axios'
import AuthService from './Auth'

class httpService {

  constructor() {
    let url = 'http://localhost:5000/api';
    this.http = axios.create({
      baseURL: url,
      headers: {
        Authorization: `Bearer ${AuthService.getToken()}`
      }
    });
  }

  getMany(url) {
    return this.http.get(url);
  }

  post(url, data) {
    return this.http.post(url, data);
  }

  delete(url, id) {
    return this.http.delete(url + '/' + id);
  }
}

let RequestService = new httpService();

export default RequestService;
