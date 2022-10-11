import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { CreateNewTask, SetParentTask, TaskDetails, TaskModel, UpdateTask } from '../models/task.models';
@Injectable({
  providedIn: 'root',
})
export class TasksService {
  constructor(private http: HttpClient) {}

  public getTask(id: string): Observable<TaskDetails> {
    return this.http.get<TaskDetails>(`/api/task/${id}`);
  }

  public getRootTasks(): Observable<TaskModel[]> {
    return this.http.get<TaskModel[]>(`api/tasks`);
  }

  public createTask(dto: CreateNewTask): Observable<TaskModel>{
    return this.http.post<TaskModel>(`api/task`, dto);
  }

  public updateTask(dto: UpdateTask): Observable<TaskModel>{
    return this.http.put<TaskModel>(`api/task`, dto);
  }

  public setParentTask(dto: SetParentTask): Observable<TaskModel>{
    return this.http.put<TaskModel>(`api/task/parent`, dto);
  }
}
