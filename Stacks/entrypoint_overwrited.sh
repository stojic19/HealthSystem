# get your envs files and export envars
export $(egrep  -v '^#'  /run/secrets/hospital_db_path | xargs) 
# if you need some specific file, where password is the secret name 
# export $(egrep  -v '^#'  /run/secrets/password| xargs) 
# call the dockerfile's entrypoint
source /docker-entrypoint.sh