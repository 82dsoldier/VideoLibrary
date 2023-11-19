import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Video } from '../common/video';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private readonly url: string = environment.apiEndpoint;
  
  constructor(private http: HttpClient) { }

  public GetPaged(start: number, count: number): Observable<Video[]> {
    return this.http.get<Video[]>(`${this.url}/Video/GetPaged?start=${start}&count=${count}`);
  } 

  public GetPagedSearch(keywords: string, start: number, count: number): Observable<Video[]> {
    return this.http.get<Video[]>(`${this.url}/Video/GetPagedSearch?keywords=${keywords}&start=${start}&count=${count}`);
  }

  public Get(videoId: number): Observable<Video> {
    return this.http.get<Video>(`${this.url}/Video/GetVideo?videoId=${videoId}`);
  }

  public GetVideoCount(): Observable<number> {
    return this.http.get<number>(`${this.url}/Video/GetVideoCount`);
  }

  public GetSearchVideoCount(keywords: string): Observable<number> {
    return this.http.get<number>(`${this.url}/Video/GetSearchVideoCount?keywords=${keywords}`);
  }
  
  public UpdateVideo(video: Video): Observable<object> {
    return this.http.put<object>(`${this.url}/Video/UpdateVideo`, video);
  }

  public UploadVideo(file: string): Observable<number> {
    return this.http.get<number>(`${this.url}/Video/UploadVideo?fileName=${file}`);
  }
}
