create table sso.Partners(
	Id uniqueidentifier not null default newId() primary key,
	Name nvarchar(250),
	SecretKey nvarchar(max),
	ClientId nvarchar(max),
	ValidateAt datetimeoffset not null,
	Status bit not null constraint df_ default 1,
	CreatedAt datetimeoffset default getutcdate(),
	UpdatedAt datetimeoffset
);