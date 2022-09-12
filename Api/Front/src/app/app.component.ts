import { Component } from '@angular/core';
import { UploadService } from './upload.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(private _service: UploadService) {}

  uploadFile(files: any, submitBtn: any): void {
    submitBtn.disabled = true;

    if (files.length === 0) {
      return;
    }

    const filesToUpload: File[] = files;
    const formData = new FormData();

    Array.from(filesToUpload).map((file, index) =>
      formData.append('file' + index, file, file.name)
    );

    this._service.upload(formData).subscribe(
      (success: any) => {
        console.log(success);
      },
      (error: any) => {
        console.log(error);
      },
      () => (submitBtn.disabled = false)
    );
  }
}
