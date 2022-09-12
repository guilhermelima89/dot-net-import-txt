import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UploadService {
  protected apiUrl: string = environment.apiUrl;

  constructor(private _httpClient: HttpClient) {}

  upload(formData: FormData): any {
    return this._httpClient.post(this.apiUrl + 'Arquivo/', formData);
  }
}
