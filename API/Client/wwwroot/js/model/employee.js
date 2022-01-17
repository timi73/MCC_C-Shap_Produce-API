$(document).ready(function () {
    $('#employeeTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                className: 'btn btn-outline-primary btn-sm',
                text: '<i class="fa fa-file-o"> Copy</i>',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'excelHtml5',
                className: 'btn btn-outline-success btn-sm',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                className: 'btn btn-outline-warning btn-sm',
                text: '<i class="fa fa-file-pdf-o"> PDF</i>',
                exportOptions: {
                    columns: [0, 2, 3, 4, 5, 6, 7]
                }
            }
        ],
        "scrollX": true,
        "ajax": {
            'url': 'Employees/RegisterGetData',
            'dataType': 'json',
            'dataSrc': ''
        },
        'columns': [
            {
                'data': null,
                'render': (data, type, row, meta) => {
                    return (meta.row + 1);
                }
            }, {
                'data': null,
                'render': (data, type, row) => {
                    return `
                        <div class="btn-group btn-group-toggle" data-toggle="buttons">
                          <label class="btn btn-info fa fa-info-circle" data-toggle="modal" data-target="#modalAddEmp" onclick="detailEmp(${row["nik"]})" title="Detail">
                            <input type="radio" name="options" id="option1" autocomplete="off">
                          </label>
                          <label class="btn btn-danger fa fa-remove" data-toggle="tooltip" onclick="deleteEmp(${row["nik"]})" title="Delete">
                            <input type="radio" name="options" id="option2" autocomplete="off" >
                          </label>
                        </div>`;
                }
            }, {
                'data': null,
                'render': (data) => {
                    return data.firstName + " " + data.lastName;
                }
            }, {
                'data': null,
                'render': (data) => {
                    if (data.gender == 0) {
                        return "Male";
                    } else {
                        return "Female";
                    }
                }
            },{
                'data': 'email'
            },{
                'data': null,
                'render': (data, type, row) => {
                    var dateGet = new Date(row['birthDate']);
                    return dateGet.toLocaleDateString();
                }
            }, {
                'data': 'phone'
            }, {
                'data': 'gpa'
            }, {
                'data': 'degree'
            }, {
                'data': 'universityName'
            }, {
                'data': 'roleName'
            }
        ]
    });
});

$('#formEmp').submit(function (e) {
    e.preventDefault();
    if ($("#btnDone").html() == 'Submit') {
        SubmitEmp();
    } else {
        updateEmp();
    }
});

function detailEmp(nik) {
    console.log(nik);
    GetUniversity();
    $.ajax({
        url: "Employees/RegisterDetailData/"+nik
    }).done((result) => {
        let data = result;
        console.log("INI DETAIL",data);
        setFormDetail(data);
    }).fail((error) => {
        console.log(error);
    });
}

function setFormDetail(data) {
    $("#password").attr("readonly", true);
    $("#email").attr("readonly", true);
    $("#btnDone").html('Update');
    $("#exampleModalLabel").html('Detail Employee');
    const myArray = data.birthDate.split("T");
    $("#nik").val(data.nik);
    $("#educationId").val(data.educationId);
    $("#firstName").val(data.firstName);
    $("#lastName").val(data.lastName);
    $("#birthDate").val(myArray[0]);
    $("#email").val(data.email);
    $("#gender").val(data.gender);
    $("#password").val(data.password);
    $("#phoneNumber").val(data.phone);
    $("#selectUniversity").val(data.universityId);
    $("#degree").val(data.degree);
    $("#GPA").val(data.gpa);
    $("#salary").val(data.salary);
}

function updateEmp() {
    var updateEmployee = new Object();
    updateEmployee.NIK = $("#nik").val();
    updateEmployee.EducationId = parseInt($("#educationId").val());
    updateEmployee.FristName = $("#firstName").val();
    updateEmployee.LastName = $("#lastName").val();
    updateEmployee.PhoneNumber = $("#phoneNumber").val();
    updateEmployee.BirthDate = $("#birthDate").val();
    updateEmployee.Salary = parseInt($("#salary").val());
    updateEmployee.Email = $("#email").val();
    updateEmployee.Gender = parseInt($("#gender").val());
    //updateEmployee.Password = $("#password").val();
    updateEmployee.Degree = $("#degree").val();
    updateEmployee.GPA = parseFloat($("#GPA").val());
    updateEmployee.UniversityId = parseInt($("#selectUniversity").val());

    console.log("INI UPDATE", updateEmployee);

    $.ajax({
        url: "Employees/UpdateRegister",
        //contentType: "application/json;charset=utf-8",
        type: "PUT",
        data: updateEmployee
    }).done((result) => {
        console.log("Result Update",result);
        Swal.fire({
            title: 'Success',
            text: result.message,
            type: 'success'
        });
        $("#formEmp")[0].reset();
        $('#modalAddEmp').modal('hide');
        $('#employeeTable').DataTable().ajax.reload();
    }).fail((error) => {
        console.log(error);
    })
}


function deleteEmp(nik) {
    Swal.fire({
        icon: 'question',
        title: 'Are you sure ?',
        text: 'Register',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Yes, Register'
    }).then((result) => {
        if (result.isDismissed == true) {
            Swal.fire({
                icon: 'error',
                title: 'Cancel',
                text: 'Canceled Deleted',
                type: 'cancel'
            });
        }
        else {
            $.ajax({
                url: "https://localhost:44365/api/Employees/"+nik,
                type: "DELETE"
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: result.message,
                    type: 'success'
                });
                $('#employeeTable').DataTable().ajax.reload();
            }).fail((error) => {
                console.log(error);
            })
        }
    });
}

function FormAdd() {
    $("#password").attr("readonly", false);
    $("#email").attr("readonly", false);
    $("#btnDone").html('Submit');
    $("#exampleModalLabel").html('Add Employee');
    $("#formEmp")[0].reset();
    GetUniversity();
}

function GetUniversity() {
    $.ajax({
        url: "Universities/GetAll"
    }).done((result) => {
        var option = "";
        $.each(result, function (key, val) {
            option += `<option value="${val.id}">${val.name}</option>`;
        });
        $("#selectUniversity").html(option);
    }).fail((error) => {
        console.log(error);
    });
}

function SubmitEmp() {
    Swal.fire({
        icon: 'question',
        title: 'Are you sure ?',
        text: 'Register',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Yes, Register'
    }).then((result) => {
        if (result.isDismissed == true) {
            Swal.fire({
                icon: 'error',
                title: 'Cancel',
                text: 'Canceled Register',
                type: 'cancel'
            });
        } else {
            var newEmployee = new Object();
            newEmployee.FristName = $("#firstName").val();
            newEmployee.LastName = $("#lastName").val();
            newEmployee.PhoneNumber = $("#phoneNumber").val();
            newEmployee.BirthDate = $("#birthDate").val();
            newEmployee.Salary = parseInt($("#salary").val());
            newEmployee.Email = $("#email").val();
            newEmployee.Gender = $("#gender").val();
            newEmployee.Password = $("#password").val();
            newEmployee.Degree = $("#degree").val();
            newEmployee.GPA = parseFloat($("#GPA").val());
            newEmployee.UniversityId = parseInt($("#selectUniversity").val());
            console.log(newEmployee);
            $.ajax({
                url: "Employees/AddRegisterData",
                //contentType: "application/json;charset=utf-8",
                type: "POST",
                data:newEmployee
            }).done((result) => {
                console.log("Result INsert", result);
                Swal.fire({
                    title: 'Success',
                    text: result.message,
                    type: 'success'
                });
                $("#formEmp")[0].reset();
                $('#modalAddEmp').modal('hide');
                $('#employeeTable').DataTable().ajax.reload();
            }).fail((error) => {
                console.log(error);
            })
        }
    });
}