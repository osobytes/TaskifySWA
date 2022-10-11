import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskDetails, TaskFormModel } from '../shared/models/task.models';
import { TasksService } from '../shared/services/tasks.service';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.scss']
})
export class TaskDetailsComponent implements OnInit {
  public taskDetails?: TaskDetails;
  public taskFormModel: TaskFormModel = {description: "", title: ""};
  public editTaskFormModel: TaskFormModel = {description: "", title: ""};
  constructor(private tasksService: TasksService, private activatedRoute: ActivatedRoute, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const taskId: string | null = params.get("id");
      if(taskId){
        this.tasksService.getTask(taskId).subscribe(taskDetails => {
          this.taskDetails = taskDetails;
        });
      }
    });
  }

  public open(content: any){
    this.modalService.open(content)
    .result.then((result) => {
      console.log(result);
    }, (reason) => {

    });
  }

}
